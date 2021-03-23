import React from 'react';
import { Switch, Route, Redirect } from 'react-router-dom';
import AddEditRecipe from './Components/RecipeList/AddEditRecipe';
import RecipeList from './Components/RecipeList/RecipeList';
import { useAuth0 } from '@auth0/auth0-react';

const Routes = () => {
  const { isAuthenticated } = useAuth0();
  return (
    isAuthenticated && (
      <Switch>
        <Route exact path="/">
          <Redirect to="/recipes">
            <RecipeList />
          </Redirect>
        </Route>
        <Route exact path="/recipes">
          <RecipeList />
        </Route>
        <Route path="/add">
          <AddEditRecipe />
        </Route>
        <Route path="/edit">
          <AddEditRecipe />
        </Route>
      </Switch>
    )
  );
};

export default Routes;
