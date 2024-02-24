import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import BaseUrl from './config';
import './CategoryForm.css';
import { ProductForm } from './ProductForm';

export const EditProduct = () => {
    const { id } = useParams();
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [picture, setPicture] = useState(null);
    const [categoryId, setCategoryId] = useState(0.0);
    const [price, setPrice] = useState(0.0);
    const [deletePicture, setDeletePicture] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        fetchProductData(id);
    }, [id]);

    const fetchProductData = async (ProductId) => {
        try {
            const response = await fetch(`${BaseUrl}/Product/Get?id=${ProductId}`);
            if (!response.ok) {
                throw new Error('Failed to fetch Product data');
            }
            const ProductData = await response.json();
            setName(ProductData.data.name);
            setPicture(ProductData.data.picture);
            setCategoryId(ProductData.data.categoryId);
            setPrice(ProductData.data.price);
            setDescription(ProductData.data.description);
        } catch (error) {
            console.error('Error fetching Product data:', error);
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const formData = new FormData();
            formData.append('id', id);
            formData.append('categoryId', categoryId);
            formData.append('name', name);
            formData.append('picture', picture);
            formData.append('description', description);
            formData.append('price', price);
            formData.append('deletePicture', deletePicture);
            const response = await fetch(`${BaseUrl}/Product/Update`,
             {
                method: 'PUT',
                body: formData,
            });

            if (!response.ok) {
                throw new Error('Failed to update Product');
            }
            navigate(`/products/${categoryId}`);
        } catch (error) {
            console.error('Error updating Product:', error);
        }
    };

    const handlePictureChange = (e) => {
        const file = e.target.files[0];
        setPicture(file);
    };

    const handleRemovePicture = () => {
        setPicture(null);
        setDeletePicture(true);
    };
    return (
        <ProductForm
            isEditing={true}
            name={name}
            setName={setName}
            description ={description}
            setDescription={setDescription}
            picture={picture}
            price={price}
            setPrice={setPrice}
            handlePictureChange={handlePictureChange}
            handleRemovePicture={handleRemovePicture}
            handleSubmit={handleSubmit}
        />
    );
}
