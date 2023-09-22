import React from 'react';
import ProductTable from '../../components/tables/productTable';

// this is what i'm talking about
function ProductPage() {
    return (
        <>
            {/* @ts-expect-error Server Component */}
            <ProductTable />
        </>);
}

export default ProductPage;
