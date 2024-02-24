import React from 'react';
import './Categories.css';
import { Link } from 'react-router-dom';

export const CategoryItem = ({ category,handleDelete }) => {
    return (
        <tr key={category.id}>
            <td>{category.id}</td>
            <td>{category.name}</td>
            <td>
                {category.picture ? (
                    <img src={category.picture} alt={category.name} />
                ) : (
                    <></>
                )}
            </td>
            <td>
                <button >
                    <Link to={`/edit/${category.id}`} className="link-no-underline">Edit</Link>
                </button>
                <button >
                    <Link to={`/products/${category.id}`} className="link-no-underline">Products</Link>
                </button>
                <button className="delete" onClick={() => handleDelete(category.id)}>Delete</button>
            </td>
        </tr>
    );
}
