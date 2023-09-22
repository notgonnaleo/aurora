import React from 'react';
import axios from 'axios';
import { Product }  from '../../types/Products/Product';
import { ProductsAPI } from '../../env';

export const getProducts = async (): Promise<Product[]> => {
    try {
        const response = await axios.get<Product[]>(`${ProductsAPI}/GetProducts`);
        return response.data;
    } catch (err) {
        console.log(err);
        return [];
    }
};