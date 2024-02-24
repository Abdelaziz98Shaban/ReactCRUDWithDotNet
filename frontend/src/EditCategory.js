import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import BaseUrl from './config';
import './CategoryForm.css';
import { CategoryForm } from './CategoryForm';

export const EditCategory = () => {
    const { id } = useParams();
    const [name, setName] = useState('');
    const [picture, setPicture] = useState(null);
    const [deletePicture, setDeletePicture] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        fetchCategoryData(id);
    }, [id]);

    const fetchCategoryData = async (categoryId) => {
        try {
            const response = await fetch(`${BaseUrl}/Category/Get?id=${categoryId}`);
            if (!response.ok) {
                throw new Error('Failed to fetch category data');
            }
            const categoryData = await response.json();
            setName(categoryData.data.name);
            setPicture(categoryData.data.picture);
        } catch (error) {
            console.error('Error fetching category data:', error);
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const formData = new FormData();
            formData.append('id', id);
            formData.append('name', name);
            formData.append('picture', picture);
            formData.append('deletePicture', deletePicture);
            const response = await fetch(`${BaseUrl}/Category/Update`,
             {
                method: 'PUT',
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
            isEditing={true}
            name={name}
            setName={setName}
            picture={picture}
            handlePictureChange={handlePictureChange}
            handleRemovePicture={handleRemovePicture}
            handleSubmit={handleSubmit}
        />
    );
}
