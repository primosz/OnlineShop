import ProductManager from './product-component.js';
const { createApp, ref, computed } = Vue

createApp({}).component('productmanager', ProductManager).mount('#app');
