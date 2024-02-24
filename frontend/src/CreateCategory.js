import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import BaseUrl from './config';
import './CategoryForm.css';
import { CategoryForm } from './CategoryForm';

export const CreateCategory = () => {
    const [name, setName] = useState('');
    const [picture, setPicture] = useState(null);
    const [deletePicture, setDeletePicture] = useState(false);
    const navigate = useNavigate();

  
    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const formData = new FormData();
            formData.append('name', name);
            formData.append('picture', picture);
            formData.append('deletePicture', deletePicture);
            const response = await fetch(`${BaseUrl}/Category/Add`,
             {
                method: 'Post',
                body: formData,
            });

            if (!response.ok) {
                throw new Error('Failed to update category');
            }
            navigate('/categories');
        } catch (error) {
            console.error('Error updating category:', error);
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
        <CategoryForm
            isEditing={false}
            name={name}
            setName={setName}
            picture={picture}
            handlePictureChange={handlePictureChange}
            handleRemovePicture={handleRemovePicture}
            handleSubmit={handleSubmit}
        />
    );
}
