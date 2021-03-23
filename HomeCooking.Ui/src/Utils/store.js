// store.js
import React, { createContext, useReducer } from 'react';

// https://blog.logrocket.com/use-hooks-and-context-not-react-and-redux/

const initialState = { color: 'red' };
const store = createContext(initialState);
const { Provider } = store;

const StateProvider = ({ children }) => {
  const [state, dispatch] = useReducer((state, action) => {
    switch (action.type) {
      case 'set_to_blue':
        return { ...state, color: 'blue' };
      default:
        throw new Error();
    }
  }, initialState);

  return <Provider value={{ state, dispatch }}>{children}</Provider>;
};

export { store, StateProvider };
