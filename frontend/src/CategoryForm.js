import React from 'react'
import { FaTrashAlt } from 'react-icons/fa';

export const CategoryForm = (
  { isEditing,
    name,
    setName,
    picture,
    handlePictureChange,
    handleRemovePicture,
    handleSubmit }) => {
  return (
    <div className="edit-category-container">
      <h2>{isEditing ? 'Edit Category' : 'Create Category'}</h2>
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
        <button type="submit">{isEditing ? 'Update' : 'Create'}</button>
      </form>
    </div>
  )
}
