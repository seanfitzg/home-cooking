## Home Cooking - a demo react application

This is an ongoing, albeit fairly basic, demo application I'm building to learn/investigate some skills and technologies required to build a full stack React -> ASP.Net Core application.

Deployed to:  https://flux-home-cooking.herokuapp.com/

The ASP.Net Core api is here -> https://github.com/seanfitzg/HomeCookingApi/tree/master/HomeCooking.Api

So far, this app uses:

- [Auth0](https://auth0.com/) to enable username authentication.
- [react-query](https://react-query.tanstack.com/) for some very basic caching
- react-hooks for managing state
- deployed to Heroku with the help of https://github.com/mars/create-react-app-buildpack

Coming soon hopefully:

- [React Testing Library](https://testing-library.com/docs/react-testing-library/intro) - I really like how RTL can help developers move a lot of their UI-based tests out of unstable e2e suites and into tests that don't rely on backend data. This really helps in situations where you are testing user interactions as opposed to api calls.
- [Mock Service Worker](https://mswjs.io/) - this works really well with react-testing-library, enabling the mocking of your api endpoints.
- [zustand](https://github.com/react-spring/zustand) - comes highly recommend from the developers of react-query. Enables the state management without the head-scratching complications of react-redux.

This project was bootstrapped with [Create React App](https://github.com/facebook/create-react-app).

## Available Scripts

In the project directory, you can run:

### `yarn start`

Runs the app in the development mode.<br />
Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

The page will reload if you make edits.<br />
You will also see any lint errors in the console.

### `yarn test`

Launches the test runner in the interactive watch mode.<br />
See the section about [running tests](https://facebook.github.io/create-react-app/docs/running-tests) for more information.

### `yarn build`

Builds the app for production to the `build` folder.<br />
It correctly bundles React in production mode and optimizes the build for the best performance.

The build is minified and the filenames include the hashes.<br />
Your app is ready to be deployed!

See the section about [deployment](https://facebook.github.io/create-react-app/docs/deployment) for more information.
