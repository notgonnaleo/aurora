export interface Product 
{
    tenantId: string; //https://stackoverflow.com/questions/49432350/how-to-represent-guid-in-typescript
    Id: string;
    SKU: string;
    Name: string;
    Description: string;
    ProductTypeId: string;
    Value: number;
    TotalWeight: number;
    LiquidWeight: number;
    Active: boolean;
    Created: Date;
    CreatedBy: string;
    Updated: Date;
    UpdatedBy: string;
}
