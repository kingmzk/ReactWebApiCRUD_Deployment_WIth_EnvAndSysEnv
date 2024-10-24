// import React, { useState, useEffect } from 'react';
// import axios from 'axios';

// // Define the Product type
// interface Product {
//   id: number;
//   name: string;
//   price: number;
//   stock: number;
// }

// //const apiUrl = 'https://localhost:7167/api/products';
// // const apiUrl = process.env.REACT_APP_API_URL || 'http://default-url';
// // const apiUrl = import.meta.env.VITE_API_URL || 'http://default-url';
// const apiUrl = import.meta.env.VITE_API_URL || 'http://default-url';

// const SampleCrud: React.FC = () => {
//     const [products, setProducts] = useState<Product[]>([]);
//     const [output, setOutput] = useState<string>('');
//     const [name, setName] = useState<string>('');
//     const [price, setPrice] = useState<number>(0);
//     const [stock, setStock] = useState<number>(0);
//     const [productId, setProductId] = useState<string>('');
//     const [error, setError] = useState<string | null>(null);
  
//     // Read all products
//     const readProducts = async () => {
//       try {
//         const response = await axios.get(apiUrl);
//         setProducts(response.data);
//         setOutput(JSON.stringify(response.data, null, 2));
//         setError(null); // Clear any previous error
//       } catch (error:any) {
//         setError('Failed to load products.');
//       }
//     };
  
//     // Create a product
//     const createProduct = async () => {
//       const newProduct = { name, price, stock };
//       try {
//         const response = await axios.post(apiUrl, newProduct, {
//           headers: { 'Content-Type': 'application/json' },
//         });
//         setOutput(JSON.stringify(response.data, null, 2));
//         readProducts(); // Fetch the updated list
//         setError(null); // Clear any previous error
//       } catch (error:any) {
//         setError('Failed to create product.');
//       }
//     };
  
//     // Update a product
//     const updateProduct = async () => {
//       const productId = prompt('Enter product ID to update:');
//       const updatedProduct = { name, price, stock };
//       try {
//         const response = await axios.put(`${apiUrl}/${productId}`, updatedProduct, {
//           headers: { 'Content-Type': 'application/json' },
//         });
//         setOutput(`Product with ID ${productId} updated.`);
//         readProducts(); // Fetch the updated list
//         setError(null); // Clear any previous error
//       } catch (error:any) {
//         if (error.response && error.response.status === 404) {
//           setError(`Product with ID ${productId} not found.`);
//         } else {
//           setError('Failed to update product.');
//         }
//       }
//     };
  
//     // Delete a product
//     const deleteProduct = async () => {
//       const productId = prompt('Enter product ID to delete:');
//       try {
//         const response = await axios.delete(`${apiUrl}/${productId}`);
//         setOutput(`Product with ID ${productId} deleted.`);
//         readProducts(); // Fetch the updated list
//         setError(null); // Clear any previous error
//       } catch (error:any) {
//         if (error.response && error.response.status === 404) {
//           setError(`Product with ID ${productId} not found.`);
//         } else {
//           setError('Failed to delete product.');
//         }
//       }
//     };
  
//     useEffect(() => {
//       readProducts();
//     }, []);
//   return (
//     <div >
//       <h1>Product CRUD Operations</h1>

//       <div>
//         <h2>Create / Update Product</h2>
//         <input
//           type="text"
//           placeholder="Product Name"
//           value={name}
//           onChange={(e) => setName(e.target.value)}
//         />
//         <input
//           type="number"
//           placeholder="Price"
//           value={price}
//           onChange={(e) => setPrice(Number(e.target.value))}
//         />
//         <input
//           type="number"
//           placeholder="Stock"
//           value={stock}
//           onChange={(e) => setStock(Number(e.target.value))}
//         />
//         <input
//           type="text"
//           placeholder="Product ID (for update)"
//           value={productId}
//           onChange={(e) => setProductId(e.target.value)}
//         />
//         <br/>
//         <button onClick={createProduct}>Create Product</button>
//         <button onClick={updateProduct}>Update Product</button>
//         <button onClick={readProducts}>Read Products</button>
//         <button onClick={deleteProduct}>Delete Product</button>
//       </div>

    

//       <div className="output">
//         <h2>Output</h2>
//         <pre>{output}</pre>
//       </div>

//       <div >
//         <h2>Product List</h2>
//         {products.map((product) => (
//           <div key={product.id}>
//             <p>ID: {product.id}</p>
//             <p>Name: {product.name}</p>
//             <p>Price: ${product.price}</p>
//             <p>Stock: {product.stock}</p>
//           </div>
//         ))}
//       </div>
//     </div>
//   );
// };

// export default SampleCrud;







import React, { useState, useEffect } from 'react';
import axios from 'axios';

// Define the Product type
interface Product {
  id: number;
  name: string;
  price: number;
  stock: number;
}

const apiUrl = import.meta.env.VITE_API_URL || 'http://default-url';

const SampleCrud: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [output, setOutput] = useState<string>('');
  const [name, setName] = useState<string>('');
  const [price, setPrice] = useState<number>(0);
  const [stock, setStock] = useState<number>(0);
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [success, setSuccess] = useState<string | null>(null);

  // Helper function to clear form inputs
  const clearForm = () => {
    setName('');
    setPrice(0);
    setStock(0);
  };

  // Read all products
  const readProducts = async () => {
    setLoading(true);
    try {
      const response = await axios.get(apiUrl);
      setProducts(response.data);
      setOutput(JSON.stringify(response.data, null, 2));
      setError(null); // Clear any previous error
    } catch (error: any) {
      setError('Failed to load products.');
    } finally {
      setLoading(false);
    }
  };

  // Create a product
  const createProduct = async () => {
    if (!name || price <= 0 || stock <= 0) {
      setError('Please fill out all fields correctly.');
      return;
    }

    const newProduct = { name, price, stock };
    setLoading(true);
    try {
      const response = await axios.post(apiUrl, newProduct, {
        headers: { 'Content-Type': 'application/json' },
      });
      setSuccess(`Product "${response.data.name}" created successfully.`);
      clearForm();
      readProducts(); // Fetch the updated list
      setError(null); // Clear any previous error
    } catch (error: any) {
      setError('Failed to create product.');
    } finally {
      setLoading(false);
    }
  };

  // Update a product
  const updateProduct = async () => {
    const productId = prompt('Enter product ID to update:');
    if (!productId) return; // Exit if no ID is provided

    if (!name || price <= 0 || stock <= 0) {
      setError('Please fill out all fields correctly.');
      return;
    }

    const updatedProduct = { name, price, stock };
    setLoading(true);
    try {
      await axios.put(`${apiUrl}/${productId}`, updatedProduct, {
        headers: { 'Content-Type': 'application/json' },
      });
      setSuccess(`Product with ID ${productId} updated successfully.`);
      clearForm();
      readProducts(); // Fetch the updated list
      setError(null); // Clear any previous error
    } catch (error: any) {
      console.error('Error:', error);
      if (error.response) {
        setError(`Error ${error.response.status}: ${error.response.statusText}`);
      } else if (error.request) {
        setError('No response received from server.');
      } else {
        setError(`Axios error: ${error.message}`);
      }
      if (error.response && error.response.status === 404) {
        setError(`Product with ID ${productId} not found.`);
      } else {
        setError('Failed to update product.');
      }
    } finally {
      setLoading(false);
    }
  };

  // Delete a product
  const deleteProduct = async () => {
    const productId = prompt('Enter product ID to delete:');
    if (!productId) return; // Exit if no ID is provided
    setLoading(true);
    try {
      await axios.delete(`${apiUrl}/${productId}`);
      setSuccess(`Product with ID ${productId} deleted successfully.`);
      readProducts(); // Fetch the updated list
      setError(null); // Clear any previous error
    } catch (error: any) {
      if (error.response && error.response.status === 404) {
        setError(`Product with ID ${productId} not found.`);
      } else {
        setError('Failed to delete product.');
      }
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    readProducts();
  }, []);


  const handleNumericInput = (setValue) => (e) => {
    const value = e.target.value;
    if (!isNaN(value) && /^\d*$/.test(value)) {
      setValue(value);
    }
  };


  return (
    <div className="container">
      <h1>Product CRUD Operations</h1>
  
      <div>
        <h2>Create / Read / Update / Delete the Products</h2>
        <input
          type="text"
          placeholder="Product Name"
          value={name}
          onChange={(e) => setName(e.target.value)}
          style={{ marginRight: '10px' }}  // Adding space between input fields
        />
       <input
          type="text"
          placeholder="Price"
          value={price}
          onChange={handleNumericInput(setPrice)}
          style={{ marginRight: '10px' }}
        />
        <input
          type="text"
          placeholder="Stock"
          value={stock}
          onChange={handleNumericInput(setStock)}
          style={{ marginRight: '10px' }}
        />
        <br />
        <div style={{ marginTop: '10px' }}>
          <button onClick={createProduct} disabled={loading} style={{ marginRight: '10px' }}>Create Product</button>
          <button onClick={updateProduct} disabled={loading} style={{ marginRight: '10px' }}>Update Product</button>
          <button onClick={readProducts} disabled={loading} style={{ marginRight: '10px' }}>Read Products</button>
          <button onClick={deleteProduct} disabled={loading}>Delete Product</button>
        </div>
      </div>
  
      {/* Success and error messages */}
      {success && <div style={{ color: 'green', marginTop: '10px' }}>{success}</div>}
      {error && <div style={{ color: 'red', marginTop: '10px' }}>{error}</div>}
      {loading && <div>Loading...</div>}
  
      {/* Product List Table */}
      <div className="table-container" style={{ marginTop: '20px' }}>
        <h2>Product List</h2>
        <table style={{ width: '100%', borderCollapse: 'collapse' }}>
          <thead>
            <tr>
              <th style={{ border: '1px solid black', padding: '10px' }}>ID</th>
              <th style={{ border: '1px solid black', padding: '10px' }}>Name</th>
              <th style={{ border: '1px solid black', padding: '10px' }}>Price</th>
              <th style={{ border: '1px solid black', padding: '10px' }}>Stock</th>
            </tr>
          </thead>
          <tbody>
            {products.map((product) => (
              <tr key={product.id}>
                <td style={{ border: '1px solid black', padding: '10px' }}>{product.id}</td>
                <td style={{ border: '1px solid black', padding: '10px' }}>{product.name}</td>
                <td style={{ border: '1px solid black', padding: '10px' }}>${product.price}</td>
                <td style={{ border: '1px solid black', padding: '10px' }}>{product.stock}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default SampleCrud;