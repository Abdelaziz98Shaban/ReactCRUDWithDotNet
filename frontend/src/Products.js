import React from 'react'
import './Categories.css';
import { ProductItem } from './ProductItem';
import { useState, useEffect } from "react";
import BaseUrl from './config';
import { Link ,useParams} from 'react-router-dom';



export const Products = () => {
    const { id } = useParams();
    const [products, setProducts] = useState([]);
    const [page, setPage] = useState(1);
    const [pageSize, setPageSize] = useState(5);

    useEffect(() => {
        fetchProducts();
    }, [page]);

    const fetchProducts = async () => {
        try {
            const response = await fetch(`${BaseUrl}/Product/GetAll?CategoryId=${id}&take=${pageSize}&skip=${(page - 1) * pageSize}`);
            if (!response.ok) {
                throw new Error('Failed to fetch Products');
            }
            const data = await response.json();
            console.log(data)
            setProducts(parseProducts(data));
        } catch (error) {
            console.error('Error fetching Products:', error);
        }
    };

    const parseProducts = (responseData) => {
        if (responseData && responseData.result && responseData.data) {
            return responseData.data;
        }
        return [];
    };

    const handleDelete = (categoryId) => {
        const isConfirmed = window.confirm('Are you sure you want to delete this category?');
        if (isConfirmed) {
            deleteCategory(categoryId);
        }
    };
    const deleteCategory = async (categoryId) => {
        try {
            const response = await fetch(`${BaseUrl}/Product/Delete?id=${categoryId}`, {
                method: 'DELETE',
            });
            if (!response.ok) {
                throw new Error('Failed to delete category');
            }
            // Update Products after successful deletion
            fetchProducts();
        } catch (error) {
            console.error('Error deleting category:', error);
        }
    };


    const handleNextPage = () => {
        setPage((prevPage) => prevPage + 1);
    };

    const handlePrevPage = () => {
        setPage((prevPage) => Math.max(prevPage - 1, 1)); // Ensure page doesn't go below 1
    };
    return (
        <div className="datatable categories-container">
            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Picture</th>
                        <th>Description</th>
                        <th>Price</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    
                {products.length > 0 ? (
                        products.map(category => (
                            <ProductItem
                                key={category.id}
                                product={category}
                                handleDelete={handleDelete}
                            />
                        ))
                    ) : (
                        <tr>
                            <td colSpan="6">No data available</td>
                        </tr>
                    )}
                </tbody>
            </table>
            <div className="pagination-buttons">
                <button onClick={handlePrevPage} disabled={page === 1}>Previous</button>
                <button onClick={handleNextPage}>Next</button>
                <button>
                    <Link to={"categories"} >Back</Link>
                </button>
            </div>
         
            <div className="create-new-button">
                <button>
                    <Link to={`/products/create/${id}`} className="create-new-link">Create New</Link>
                </button>
            </div>
        </div>
    );
}
