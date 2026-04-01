export default function TodoItem({ todo, deleteTodo, toggleTodo }) {
  return (
    <div style={{ margin: "10px" }}>
      <input
        type="checkbox"
        checked={todo.completed}
        onChange={() => toggleTodo(todo.id)}
      />

      <span
        style={{
          marginLeft: "10px",
          textDecoration: todo.completed ? "line-through" : "none",
        }}
      >
        {todo.text}
      </span>

      <button
        onClick={() => deleteTodo(todo.id)}
        style={{ marginLeft: "10px" }}
      >
        ❌
      </button>
    </div>
  );
}