<template>
    <div>
        <div v-if="!editing">
            <button class="button" @click="newProduct">Add new Product</button>
            <table class="table">
                <tr>
                    <th>Id</th>
                    <th>Product</th>
                    <th>Value</th>
                    <th></th>
                    <th></th>
                </tr>
                <tr v-for="(product, index) in products" :key="product.id">
                    <td>{{ product.id }}</td>
                    <td>{{ product.name }}</td>
                    <td>{{ product.value }}</td>
                    <td>
                        <button class="button is-info" @click="editProduct(product.id, index)">Edit</button>
                    </td>
                    <td>
                        <button class="button is-error" @click="deleteProduct(product.id, index)">Remove</button>
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
        <div v-else>
            <div class="field">
                <label class="label">Product Name</label>
                <div class="control">
                    <input class="input" v-model="productModel.name" />
                </div>
            </div>

            <div class="field">
                <label class="label">Product Description</label>

                <div class="control">
                    <input class="input" v-model="productModel.description" />
                </div>
            </div>

            <div class="field">
                <label class="label">Product Value</label>

                <div class="control">
                    <input class="input" v-model="productModel.value" />
                </div>
            </div>
            <div class="buttons">
                <button class="button is-success" @click="createProduct" v-if="!productModel.id">Create Product</button>
                <button class="button is-warning" @click="updateProduct" v-else>Update Product</button>
                <button class="button " @click="cancel">Cancel</button>
            </div>
        </div>
    </div>
</template>

<script>
import { ref, watchEffect } from 'vue';
import axios from 'axios';

export default {
  name: 'ProductManager',
  setup() {
    const editing = ref(false);
    const message = ref('Admin Panel');
    const loading = ref(false);
    const objectIndex = ref(0);
    const productModel = ref({
      id: 0,
      name: 'ProductName',
      description: 'ProductDescription',
      value: 6.66,
    });
    const products = ref([]);

    function getProduct(id) {
      loading.value = true;
      axios
        .get('/Admin/products/' + id)
        .then((res) => {
          console.log(res);
          const product = res.data;
          productModel.value = {
            id: product.id,
            name: product.name,
            description: product.description,
            value: product.value,
          };
        })
        .catch((err) => {
          console.error(err);
        })
        .then(() => {
          loading.value = false;
        });
    }

    function getProducts() {
      loading.value = true;
      axios
        .get('/Admin/products')
        .then((res) => {
          console.log(res);
          products.value = res.data;
        })
        .catch((err) => {
          console.error(err);
        })
        .then(() => {
          loading.value = false;
        });
    }

    function createProduct() {
      loading.value = true;
      axios
        .post('/Admin/products', productModel.value)
        .then((res) => {
          console.log(res.data);
          products.value.push(res.data);
        })
        .catch((err) => {
          console.error(err);
        })
        .then(() => {
          loading.value = false;
          editing.value = false;
        });
    }

    function updateProduct() {
      loading.value = true;
      axios
        .put('/Admin/products', productModel.value)
        .then((res) => {
          console.log('Updating object index: ' + objectIndex.value);
          products.value.splice(objectIndex.value, 1, res.data);
        })
        .catch((err) => {
          console.error(err);
        })
        .then(() => {
          loading.value = false;
          editing.value = false;
        });
    }

    function deleteProduct(id, index) {
      loading.value = true;
      axios
        .delete('/Admin/products/' + id)
        .then((res) => {
          console.log(res);
          products.value.splice(index, 1);
        })
        .catch((err) => {
          console.error(err);
        })
        .then(() => {
          loading.value = false;
        });
    }

    function newProduct() {
      editing.value = true;
      productModel.value.id = 0;
    }

    function editProduct(id, index) {
      objectIndex.value = index;
      getProduct(id);
      editing.value = true;
    }

    function cancel() {
      editing.value = false;
    }

    watchEffect(() => {
      if (!editing.value) {
        getProducts();
      }
    });

    return {
      message,
      editing,
      objectIndex,
      productModel,
      getProduct,
      getProducts,
      createProduct,
      editProduct,
      updateProduct,
      deleteProduct,
      products,
      newProduct,
      cancel,
      loading,
    };
  },
  mounted() {
    console.log('Mounted');
  },
};
</script>
