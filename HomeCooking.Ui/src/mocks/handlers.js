import { rest } from 'msw';

export const handlers = [
  rest.get('http://localhost:5000/recipes', (req, res, ctx) => {
    return res(
      ctx.json([
        { id: 1, name: 'Test Recipe', description: 'Test Description' },
      ])
    );
  }),
  rest.get('http://localhost:5000/recipes/1', (req, res, ctx) => {
    return res(
      ctx.json({
        id: 1,
        name: 'Test Recipe',
        description: 'Test Description',
        method: 'Test Method',
      })
    );
  }),
];
