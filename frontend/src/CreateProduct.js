import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import BaseUrl from './config';
import './CategoryForm.css';
import { ProductForm } from './ProductForm';
import { useParams} from 'react-router-dom';

export const CreateProduct = () => {
    const { id } = useParams();
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [price, setPrice] = useState(0.0);
    const [picture, setPicture] = useState(null);
    const [deletePicture, setDeletePicture] = useState(false);
    const navigate = useNavigate();

  
    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const formData = new FormData();
            formData.append('categoryId', id);
            formData.append('name', name);
            formData.append('picture', picture);
            formData.append('description', description);
            formData.append('price', price);
            formData.append('deletePicture', deletePicture);
            const response = await fetch(`${BaseUrl}/Product/Add`,
             {
                method: 'Post',
                body: formData,
            });

            if (!response.ok) {
                throw new Error('Failed to update Product');
            }
            navigate(`/products/${id}`);
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
            isEditing={false}
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
