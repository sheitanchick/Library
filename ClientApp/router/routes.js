import Users from 'components/users'
import EditUser from 'components/edit-user'
import Books from 'components/books'
import AddUser from 'components/add-user'
import EditBook from 'components/edit-book'
import AddBook from 'components/add-book'
import BookOrdering from 'components/book-ordering'
import Orders from 'components/orders'
 
export const routes = [
  { name: 'users', path: '/', component: Users, display: 'Customers' },
  { name: 'edit-user', path: '/edit-user/:id', component: EditUser, display: 'Edit User', hide: true },
  { name: 'add-user', path: '/add-user', component: AddUser, display: 'Add User' },
  { name: 'books', path: '/books', component: Books, display: 'Books' },
  { name: 'edit-book', path: '/edit-book/:id', component: EditBook, display: 'Edit Book', hide: true },
  { name: 'add-book', path: '/add-book', component: AddBook, display: 'Add Book' },
  { name: 'book-ordering', path: '/book-ordering', component: BookOrdering, display: 'Order a book' },
  { name: 'orders', path: '/orders', component: Orders, display: 'Orders' },

]
