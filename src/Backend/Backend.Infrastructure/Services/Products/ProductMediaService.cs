using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Enums.Localization;
using Backend.Infrastructure.Services.Authentication;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Base;
using Backend.Infrastructure.Services.Categories;
using Backend.Infrastructure.Services.ProductTypes;
using Backend.Infrastructure.Services.SubCategories;
using Backend.Infrastructure.Services.Tenants;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Infrastructure.Services.Products
{
    public class ProductMediaService : Service
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public ProductMediaService(AppDbContext appDbContext, IConfiguration configuration, UserContextService main)
            : base(main)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        public IEnumerable<ProductMedia> Get(Guid productId)
        {
            var context = LoadContext();
            return _appDbContext.ProductMedia
                .Where(x => x.TenantId == context.Tenant.Id && x.ProductId == productId && x.Active)
                .ToList();
        }

        public ProductMedia? GetById(Guid id)
        {
            var context = LoadContext();
            return _appDbContext.ProductMedia
                .FirstOrDefault(x => x.TenantId == context.Tenant.Id && x.Id == id && x.Active);
        }

        public async Task<ProductMedia> Add(ProductMedia model)
        {

            var context = LoadContext();
            model.TenantId = context.Tenant.Id;
            if (!string.IsNullOrEmpty(model.ImageBuffer))
            {
               model.MediaURL = await UploadFile(model.ImageBuffer);
            }

            if (!string.IsNullOrEmpty(model.MediaURL))
            {
                model.Active = true;
                _appDbContext.ProductMedia.Add(model);
                if (await _appDbContext.SaveChangesAsync() > 0)
                    return model;
            }

            throw new Exception(Localization.GenericValidations.ErrorSaveItem(context.Language));
        }

        public async Task<string> UploadFile(string base64string)
        {
            byte[] data;

            try
            {
                data = Convert.FromBase64String(base64string);
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid base64 string");
            }

            var context = LoadContext();

            // Definições do Blob Storage
            var blobContainerClient = new BlobContainerClient(
                "DefaultEndpointsProtocol=https;AccountName=appaurorablobstorage;AccountKey=LedXk/66YH5xRnk7fUFau6CR1ID2cu87Qek2Jnx55EExzYCgSg4rqK4VkIbZpN6siSUf8qxzPrA++ASttiIX1w==;EndpointSuffix=core.windows.net",
                "assets"
            );

            var blobHttpHeader = new BlobHttpHeaders { ContentType = "image/jpeg" };
            var blobClient = blobContainerClient.GetBlobClient($"{context.Tenant.Name!.ToLower()}/images/{Guid.NewGuid()}.jpg");

            try
            {
                using (var ms = new MemoryStream(data, false))
                {
                    var response = await blobClient.UploadAsync(ms, blobHttpHeader);
                    if (response.GetRawResponse().Status == 201)
                    {
                        return blobClient.Uri.AbsoluteUri;
                    }
                }
            }
            catch (Exception ex)
            {
                // Logar o erro
                // _logger.LogError(ex, "Error uploading file to blob storage");
                throw new Exception("Image failed during upload process", ex);
            }

            throw new Exception("Image failed during upload process");
        }


        public bool Update(ProductMedia model)
        {
            var context = LoadContext();
            if (model.TenantId != context.Tenant.Id) throw new Exception(Localization.GenericValidations.ErrorWrongTenant(context.Language));

            model.Updated = DateTime.UtcNow;
            model.UpdatedBy = context.UserId;
            _appDbContext.Update(model);
            return _appDbContext.SaveChanges() > 0;
        }

        public bool Delete(Guid id)
        {
            var context = LoadContext();

            ProductMedia? model = _appDbContext.ProductMedia.FirstOrDefault(x => x.Id == id && x.TenantId == context.Tenant.Id) ?? throw new Exception(Localization.GenericValidations.ErrorDeleteItem(context.Language));

            model.Active = false;
            _appDbContext.Update(model);
            return _appDbContext.SaveChanges() > 0;
        }

        

    }
}
