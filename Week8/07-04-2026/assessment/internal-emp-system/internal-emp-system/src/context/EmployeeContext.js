import { createContext, useReducer, useContext } from "react";

const EmployeeContext = createContext();

const initialState = {
  employees: [],
};

function reducer(state, action) {
  switch (action.type) {
    case "ADD":
      return { employees: [...state.employees, action.payload] };

    case "UPDATE":
      return {
        employees: state.employees.map(emp =>
          emp.id === action.payload.id ? action.payload : emp
        ),
      };

    case "DELETE":
      return {
        employees: state.employees.filter(emp => emp.id !== action.payload),
      };

    default:
      return state;
  }
}

export const EmployeeProvider = ({ children }) => {
  const [state, dispatch] = useReducer(reducer, initialState);

  return (
    <EmployeeContext.Provider
      value={{
        employees: state.employees,
        add: emp => dispatch({ type: "ADD", payload: emp }),
        update: emp => dispatch({ type: "UPDATE", payload: emp }),
        remove: id => dispatch({ type: "DELETE", payload: id }),
      }}
    >
      {children}
    </EmployeeContext.Provider>
  );
};

export const useEmployees = () => useContext(EmployeeContext);