// store.js
import React, { createContext, useReducer } from 'react';

// https://blog.logrocket.com/use-hooks-and-context-not-react-and-redux/

const initialState = { color: 'red', recipe: { name: '', ingredients: [] } };
const store = createContext(initialState);
const { Provider } = store;

const StateProvider = ({ children }) => {
  const [state, dispatch] = useReducer((state, action) => {
    switch (action.type) {
      case 'set_to_blue':
        return { ...state, color: 'blue' };
      case 'SET_RECIPE':
        return { ...state, recipe: action.recipe };
      case 'UPDATE_INGREDIENTS':
        return {
          ...state,
          recipe: { ...state.recipe, ingredients: action.ingredients },
        };
      default:
        throw new Error();
    }
  }, initialState);

  return <Provider value={{ state, dispatch }}>{children}</Provider>;
};

export { store, StateProvider };
