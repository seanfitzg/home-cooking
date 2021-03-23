import React from 'react';
import { useAuth0 } from '@auth0/auth0-react';
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import FastfoodIcon from '@material-ui/icons/Fastfood';
import { useHistory } from 'react-router-dom';
import LogoutButton from '../../Authentication/LogoutButton';
import LoginButton from '../../Authentication/LoginButton';

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  },
  menuButton: {
    marginRight: theme.spacing(2),
  },
  title: {
    flexGrow: 1,
  },
}));

export default function RecipeAppBar() {
  const history = useHistory();
  const classes = useStyles();
  const { isAuthenticated } = useAuth0();

  return (
    <div className={classes.root}>
      <AppBar position="static">
        <Toolbar>
          <FastfoodIcon className={classes.menuButton} />
          <Typography variant="h6" className={classes.title}>
            Home Cooking
          </Typography>
          {isAuthenticated && (
            <Button onClick={() => history.push('/recipes')} color="inherit">
              Recipes
            </Button>
          )}
          {isAuthenticated && <LogoutButton />}
          {!isAuthenticated && <LoginButton />}
        </Toolbar>
      </AppBar>
    </div>
  );
}
