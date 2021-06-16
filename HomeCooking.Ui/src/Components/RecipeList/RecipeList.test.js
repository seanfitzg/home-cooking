import { render, fireEvent, waitFor, screen } from '../../Utils/test-utils';
import { server } from '../../mocks/server';
import '@testing-library/jest-dom/extend-expect';
import RecipeList from './RecipeList';

jest.mock('@auth0/auth0-react', () => {
  return {
    useAuth0: () => ({
      isAuthenticated: true,
      getAccessTokenSilently: () => {},
    }),
  };
});

beforeAll(() => server.listen());
afterEach(() => server.resetHandlers());
afterAll(() => server.close());

test('loads the recipe list', async () => {
  render(<RecipeList />);
  await waitFor(() => {
    expect(
      screen.getByText('Test Recipe', { exact: false })
    ).toBeInTheDocument();
  });
});
