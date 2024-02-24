import React from 'react';
import './Categories.css';
import { Link } from 'react-router-dom';

export const ProductItem = ({ product,handleDelete }) => {
    return (
        <tr key={product.id}>
            <td>{product.id}</td>
            <td>{product.name}</td>
        
            <td>
                {product.picture ? (
                    <img src={product.picture} alt={product.name} />
                ) : (
                    <></>
                )}
            </td>
            <td>{product.description}</td>
            <td>{product.price}</td>
            <td>
                <button >
                    <Link to={`/products/editProduct/${product.id}`} className="link-no-underline">Edit</Link>
                </button>
                <button className="delete" onClick={() => handleDelete(product.id)}>Delete</button>
            </td>
        </tr>
    );
}
