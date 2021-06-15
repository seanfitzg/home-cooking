import React, { useState, useEffect, useContext } from 'react';
import { store } from '../../Utils/store';
import { useAuth0 } from '@auth0/auth0-react';
import queryString from 'query-string';
import Grid from '@material-ui/core/Grid';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import { useHistory } from 'react-router-dom';
import { makeStyles } from '@material-ui/core/styles';
import postRecipe from '../../Api/postRecipe';
import updateRecipe from '../../Api/updateRecipe';
import getRecipeById from '../../Api/getRecipeById';
import Ingredients from '../Ingredients/Ingredients';

const useStyles = makeStyles((theme) => ({
  grid: {
    marginTop: '20px',
    backgroundColor: theme.palette.background.paper,
  },
  add: {
    marginRight: '20px',
  },
}));

const AddEditRecipe = () => {
  const classes = useStyles();
  const storeContext = useContext(store);
  const { getAccessTokenSilently } = useAuth0();
  const history = useHistory();
  //const [recipe, setRecipe] = useState({ name: '', method: '' });
  const { state, dispatch } = storeContext;

  const recipe = state.recipe ?? {};

  const addUpdateRecipe = async () => {
    let result;
    if (recipe.id) {
      result = await updateRecipe(recipe, getAccessTokenSilently);
    } else {
      result = await postRecipe(recipe, getAccessTokenSilently);
    }
    if (result.ok) {
      history.push('/recipes');
    }
  };

  const setRecipe = (recipe) => {
    dispatch({ type: 'SET_RECIPE', recipe });
  };

  const getRecipe = async (id) => {
    const recipe = await getRecipeById(getAccessTokenSilently, id);
    setRecipe(recipe);
  };

  useEffect(() => {
    const parsed = queryString.parse(window.location.search);
    if (parsed.id) {
      getRecipe(parsed.id);
    }
  }, []);

  return (
    <form>
      <Grid
        container
        direction="column"
        justify="center"
        alignItems="center"
        className={classes.grid}
      >
        <TextField
          id="outlined-full-width"
          label="Name"
          style={{ margin: 8 }}
          fullWidth
          margin="normal"
          InputLabelProps={{
            shrink: true,
          }}
          value={recipe.name}
          onChange={(evt) => setRecipe({ ...recipe, name: evt.target.value })}
          variant="outlined"
        />
        <TextField
          id="outlined-full-width"
          label="Description"
          style={{ margin: 8 }}
          fullWidth
          multiline
          margin="normal"
          InputLabelProps={{
            shrink: true,
          }}
          inputProps={{
            style: {
              height: '50px',
              padding: '0 14px',
            },
          }}
          value={recipe.description}
          onChange={(evt) =>
            setRecipe({ ...recipe, description: evt.target.value })
          }
          variant="outlined"
        />
        <TextField
          id="outlined-full-width"
          label="Method"
          style={{ margin: 8 }}
          fullWidth
          multiline
          margin="normal"
          InputLabelProps={{
            shrink: true,
          }}
          inputProps={{
            style: {
              height: '200px',
              padding: '0 14px',
            },
          }}
          value={recipe.method}
          onChange={(evt) => setRecipe({ ...recipe, method: evt.target.value })}
          variant="outlined"
        />

        <Ingredients ingredients={state.ingredients} />

        <Grid
          container
          direction="row"
          justify="center"
          alignItems="center"
          className={classes.grid}
        >
          <Button
            variant="outlined"
            color="primary"
            onClick={addUpdateRecipe}
            className={classes.add}
          >
            {recipe.id ? <>Update</> : <>Add</>}
          </Button>
          <Button
            variant="outlined"
            color="primary"
            onClick={() => history.push('/recipes')}
          >
            Cancel
          </Button>
        </Grid>
      </Grid>
    </form>
  );
};

export default AddEditRecipe;
