import { Categories } from "./Categories";
import { Header } from './Header';
import { Footer } from './Footer';
import {EditCategory} from './EditCategory';
import {CreateCategory} from './CreateCategory';
import {CreateProduct} from './CreateProduct';
import {EditProduct} from './EditProduct';
import {Products} from './Products';
import { Routes, Route } from 'react-router-dom';

function App() {



  return (
    <div className="App">
      <Header />

      <Routes>
        <Route path="*" element={<Categories />} />
        <Route path="/edit/:id" element={<EditCategory />} />
        <Route path="/products/:id" element={<Products />} />
        <Route path="/create" element={<CreateCategory />} />
        <Route path="products/create/:id" element={<CreateProduct />} />
        <Route path="products/editProduct/:id" element={<EditProduct />} />
      </Routes>


      <Footer />


    </div>
  );
}

export default App;
