import React from 'react'
import { FaTrashAlt } from 'react-icons/fa';

export const ProductForm = (
  { isEditing,
    name,
    setName,
    description,
    setDescription,
    picture,
    price,
    setPrice,
    handlePictureChange,
    handleRemovePicture,
    handleSubmit }) => {
  return (
    <div className="edit-category-container">
      <h2>{isEditing ? 'Edit Product' : 'Create Product'}</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="name">Name:</label>
          <input
            type="text"
            id="name"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
          />
        </div>
        <div>
          <label htmlFor="description">Description:</label>
          <input
            type="text"
            id="description"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            required
          />
        </div>
        <div>
          <label htmlFor="picture">Picture:</label>
          <input
            type="file"
            id="picture"
            accept="image/*"
            onChange={handlePictureChange}
          />
          {picture && (
            <div style={{ position: 'relative', display: 'inline-block' }}>
              <img
                src={picture instanceof File ? URL.createObjectURL(picture) : picture}
                alt="Selected"
                style={{ maxWidth: '100px', maxHeight: '100px' }}
              />
              <button
                type="button"
                onClick={handleRemovePicture}
                style={{
                  position: 'absolute',
                  top: '5px',
                  right: '5px',
                  background: 'transparent',
                  border: 'none',
                  cursor: 'pointer',
                }}
              >
                <FaTrashAlt />
              </button>
            </div>
          )}
        </div>   
        <div>
          <label htmlFor="price">Price:</label>
          <input
            type="num"
            id="price"
            value={price}
            onChange={(e) => setPrice(e.target.value)}
            required
          />
        </div>

        <button type="submit">{isEditing ? 'Update' : 'Create'}</button>
      </form>
    </div>
  )
}
