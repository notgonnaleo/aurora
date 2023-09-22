import { useEffect, useState } from 'react';
import { getProducts } from '../../services/Products/productsService';
import { Product } from '../../types/Products/Product';

const ProductTable = async () => {
    const [products, setProducts] = useState<Product[]>();

    let fetchData = async () => {
        const data = await getProducts();
        setProducts(data);
    }

    useEffect(() => {
        fetchData();
    }, []);

    // This stuff below are just for tests, I intend on creating
    // an exclusive method to build tables and all that stuff.
    // meanwhile this here is just to make things work but the idea might be the same.

    /**
     * Generic method to fetch the object columns
     * @param model
     * @returns
     */
    let getColumns = (model: Product) => {
        let columns = Object.keys(model);
        return columns;
    }

    /**
     * Method to build the table component
     * @param products
     * @returns
     */

    // I will change it, this looks horrible when we go to the products page.
    let buildTable = (products: Product[]) => {
        return (<div>
            <table>
                <thead>
                    <tr>
                        {getColumns(products[0]).map((column, index) => (
                            <th key={index}>{column}</th>
                        ))}
                    </tr>
                </thead>
                <tbody>
                    {products.map((value, key) =>
                        <tr key={key}>{value.Id}</tr>
                    )}
                </tbody>
            </table>
        </div>
        );
    }
    return buildTable 
}
export default ProductTable;
