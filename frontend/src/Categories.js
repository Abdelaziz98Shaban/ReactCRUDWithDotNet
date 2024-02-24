import React from 'react'
import './Categories.css';
import { CategoryItem } from './CategoryItem';
import { useState, useEffect } from "react";
import BaseUrl from './config';
import { Link } from 'react-router-dom';



export const Categories = () => {
    const [categories, setCategories] = useState([]);
    const [page, setPage] = useState(1);
    const [pageSize, setPageSize] = useState(5);

    useEffect(() => {
        fetchCategories();
    }, [page]);

    const fetchCategories = async () => {
        try {
            const response = await fetch(`${BaseUrl}/Category/GetAll?take=${pageSize}&skip=${(page - 1) * pageSize}`);
            if (!response.ok) {
                throw new Error('Failed to fetch categories');
            }
            const data = await response.json();
            setCategories(parseCategories(data));
        } catch (error) {
            console.error('Error fetching categories:', error);
        }
    };

    const parseCategories = (responseData) => {
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
            const response = await fetch(`${BaseUrl}/Category/Delete?id=${categoryId}`, {
                method: 'DELETE',
            });
            if (!response.ok) {
                throw new Error('Failed to delete category');
            }
            // Update categories after successful deletion
            fetchCategories();
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
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    
                {categories.length > 0 ? (
                        categories.map(category => (
                            <CategoryItem
                                key={category.id}
                                category={category}
                                handleDelete={handleDelete}
                            />
                        ))
                    ) : (
                        <tr>
                            <td colSpan="4">No data available</td>
                        </tr>
                    )}
                </tbody>
            </table>
            <div className="pagination-buttons">
                <button onClick={handlePrevPage} disabled={page === 1}>Previous</button>
                <button onClick={handleNextPage}>Next</button>
            </div>
            <div className="create-new-button">
                <button>
                    <Link to="/create" className="create-new-link">Create New</Link>
                </button>
            </div>
        </div>
    );
}
