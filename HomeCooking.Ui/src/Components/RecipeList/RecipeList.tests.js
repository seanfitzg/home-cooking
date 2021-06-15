import { render, fireEvent, waitFor, screen } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import RecipeList from './RecipeList';

test('loads the recipe list', async () => {
  render(<RecipeList />);
});
