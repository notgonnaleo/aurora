﻿using Backend.Domain.Entities.Profiles;
using Backend.Domain.Entities.Stocks;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Client;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;
using System.Net.Http.Json;
using StockEnums = Backend.Infrastructure.Enums.Modules.Methods.Stock;


namespace Frontend.Web.Repository.Stocks
{
    public class StockRepository
    {
        private readonly HttpClientRepository _httpClientRepository;

        public StockRepository(HttpClientRepository httpClientRepository)
        {
            _httpClientRepository = httpClientRepository;
        }

        public async Task<ApiResponse<Stock>> CreateStock(Stock stock)
        {
            var model = new RouteBuilder<Stock>().Send(Endpoints.Stock, Methods.Default.POST, stock);
            return await _httpClientRepository.Post<Stock, Stock>(model);
        }

        public async Task<IEnumerable<StockDetail>> GetStockWithDetail(string tenantId)
        {
            var parameters = new RouteParameterRequest() { ParameterName = StockEnums.GET.GetStockWithDetail.Args.tenantId, ParameterValue = tenantId };
            var request = new RouteBuilder<StockDetail>().Send(Endpoints.Stock, StockEnums.GET.GetStockWithDetail.GetStockWithDetailEndpoint, parameters);
            return (await _httpClientRepository.Get(request)).Result;
        }
        public async Task<IEnumerable<Inventory>> GetInventory(string tenantId)
        {
            var parameters = new RouteParameterRequest() { ParameterName = StockEnums.GET.GetInventory.Args.tenantId, ParameterValue = tenantId };
            var request = new RouteBuilder<Inventory>().Send(Endpoints.Stock, StockEnums.GET.GetInventory.GetInventoryEndpointName, parameters);
            return (await _httpClientRepository.Get(request)).Result;
        }
        public async Task<IEnumerable<Inventory>> GetProductStock(string tenantId, string productId, string variantId)
        {
            var parameters = new List<RouteParameterRequest>()
            {
                new RouteParameterRequest()
                {
                    ParameterName = StockEnums.GET.GetProductStock.Args.tenantId,
                    ParameterValue = tenantId,
                },
                new RouteParameterRequest()
                {
                    ParameterName = StockEnums.GET.GetProductStock.Args.productId,
                    ParameterValue = productId,
                },
                new RouteParameterRequest()
                {
                    ParameterName = StockEnums.GET.GetProductStock.Args.variantId,
                    ParameterValue = variantId,
                }
            };
            var request = new RouteBuilder<Inventory>().SendMultiple(Endpoints.Stock, StockEnums.GET.GetProductStock.GetProductStockEndpointName, parameters);
            return (await _httpClientRepository.Get(request)).Result;
        }

        public async Task<IEnumerable<Stock>> GetStocks(string tenantId)
        {
            var parameters = new RouteParameterRequest() { ParameterName = StockEnums.GET.GetStocks.tenantId, ParameterValue = tenantId };
            var request = new RouteBuilder<Stock>().Send(Endpoints.Stock, Methods.Default.GET, parameters);
            return (await _httpClientRepository.Get(request)).Result;
        }

        public async Task<Stock> GetStock(string tenantId, string stockMovementId)
        {
            var parameters = new List<RouteParameterRequest>()
            {
                new RouteParameterRequest()
                {
                    ParameterName = StockEnums.GET.GetStock.tenantId,
                    ParameterValue = tenantId,
                },
                new RouteParameterRequest()
                {
                    ParameterName = StockEnums.GET.GetStock.stockMovementId,
                    ParameterValue = stockMovementId,
                }
            };
            var request = new RouteBuilder<Stock>().SendMultiple(Endpoints.Stock, Methods.Default.FIND, parameters);
            return (await _httpClientRepository.Find(request)).Result;
        }

        public async Task<bool> UpdateStock(Stock stock)
        {
            var model = new RouteBuilder<Stock>().Send(Endpoints.Stock, Methods.Default.PUT, stock);
            return (await _httpClientRepository.Put(model)).Success;
        }

        public async Task<bool> DeleteStock(string tenantId, string stockMovementId)
        {
            var parameters = new List<RouteParameterRequest>()
                {
                    new RouteParameterRequest()
                    {
                        ParameterName = Methods.Stock.DELETE.DeleteStock.tenantId,
                        ParameterValue = tenantId,
                    },
                    new RouteParameterRequest()
                    {
                        ParameterName = Methods.Stock.DELETE.DeleteStock.StockMovementId,
                        ParameterValue = stockMovementId,
                    }
                };
            var request = new RouteBuilder<Stock>().SendMultiple(Endpoints.Stock, Methods.Default.DELETE, parameters);
            return (await _httpClientRepository.Put(request)).Success;
        }

        public async Task<IEnumerable<StockDetail>> GetStockEntriesByProduct(string tenantId, string productId)
        {
            var parameters = new List<RouteParameterRequest>()
            {
                new RouteParameterRequest()
                {
                    ParameterName = StockEnums.GET.GetStockEntriesByProduct.Args.tenantId,
                    ParameterValue = tenantId,
                },
                new RouteParameterRequest()
                {
                    ParameterName = StockEnums.GET.GetStockEntriesByProduct.Args.productId,
                    ParameterValue = productId,
                }
            };
            var request = new RouteBuilder<StockDetail>().SendMultiple(Endpoints.Stock, StockEnums.GET.GetStockEntriesByProduct.GetStockEntriesByProductEndpointName, parameters);
            return (await _httpClientRepository.Get(request)).Result;
        }

    }
}
