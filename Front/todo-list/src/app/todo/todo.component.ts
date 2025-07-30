import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TodoService } from '../services/todo.service';
import { Todo } from '../models/todo';
@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.css'],
  standalone: true,
  imports: [FormsModule,CommonModule],
})
export class TodoComponent  implements OnInit{
  todos: Todo[] = [];
  newTodo: string = '';

  todoForm: Todo = {
    text: '',
    date: ""
  };
  
  ngOnInit(): void {
    this.loadTodos();
  }
  loadTodos(): void {
    this.todoService.getTodos().subscribe(data => {
      this.todos = data;
    });
  }
  constructor(private todoService: TodoService) { }

  addTodo() {
    const todo = this.newTodo.trim();
    if (todo) {
      this.todoForm.text = todo;
      this.todoForm.date = new Date().toISOString();

      this.todoService.createTodo(this.todoForm).subscribe(() => {
        this.loadTodos();
        this.newTodo = '';
      });
    }
  }

  removeTodo(id?: string) {
    if (!id) return;
    if (confirm('آیا مطمئنید حذف شود؟')) {
      this.todoService.deleteTodo(id).subscribe(() => {
        this.loadTodos();
      });
    }
  }
}
