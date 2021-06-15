import React, { useContext } from 'react';
import { store } from '../../Utils/store';
import { v4 as uuidv4 } from 'uuid';
import { useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { DataGrid } from '@material-ui/data-grid';
import TextField from '@material-ui/core/TextField';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';

const useStyles = makeStyles((theme) => ({
  title: {
    marginBottom: 5,
  },
  container: {
    width: '100%',
  },
  ingredientHeader: {
    marginTop: 15,
    marginBottom: 10,
  },
  textBoxes: {
    '& > *': {
      marginLeft: 10,
      width: '25ch',
    },
  },
  deleteButton: {
    marginTop: 10,
  },
}));

const columns = [
  { field: 'item', headerName: 'Item', width: 400 },
  { field: 'amount', headerName: 'Amount', width: 200 },
];

export default function Ingredients() {
  const classes = useStyles();
  const storeContext = useContext(store);
  const [item, setItem] = useState('');
  const [amount, setAmount] = useState('');
  const [deleteDisabled, setDeleteDisabled] = useState(true);
  const [itemsToDelete, setItemsToDelete] = useState([]);

  const { state, dispatch } = storeContext;

  const ingredientList = state.recipe ? state.recipe.ingredients ?? [] : [];

  const handleAdd = () => {
    if (!item) return;
    dispatch({
      type: 'UPDATE_INGREDIENTS',
      ingredients: [
        ...state.recipe.ingredients,
        { id: uuidv4(), item, amount, isNew: true },
      ],
    });
    setAmount('');
    setItem('');
  };

  const handleDelete = () => {
    var remaining = ingredientList.filter(
      (el) => !itemsToDelete.includes(el.id)
    );
    dispatch({
      type: 'UPDATE_INGREDIENTS',
      ingredients: remaining,
    });
    setDeleteDisabled(true);
  };

  const checkBoxSelection = (item) => {
    console.log(`item`, item);
    setDeleteDisabled(item.selectionModel?.length > 0 ? false : true);
    setItemsToDelete(item.selectionModel);
  };

  return (
    <>
      <div className={classes.container}>
        <Grid
          className={classes.ingredientHeader}
          container
          direction="row"
          justify="space-between"
          alignItems="center"
        >
          <div className={classes.title}>Ingredients</div>
          <div className={classes.textBoxes}>
            <TextField
              id="amount"
              label="Amount"
              value={amount}
              onChange={(event) => setAmount(event.target.value)}
            />
            <TextField
              id="item"
              label="Item"
              value={item}
              onChange={(event) => setItem(event.target.value)}
            />
            <Button onClick={handleAdd} variant="contained">
              Add Ingredient
            </Button>
          </div>
        </Grid>
        <div style={{ width: '100%' }}>
          <DataGrid
            rows={ingredientList}
            columns={columns}
            pageSize={100}
            checkboxSelection
            autoHeight
            autoPageSize
            hideFooterPagination
            onSelectionModelChange={(item) => checkBoxSelection(item)}
          />
        </div>
        <Button
          disabled={deleteDisabled}
          onClick={handleDelete}
          variant="contained"
          className={classes.deleteButton}
        >
          Delete Ingredients
        </Button>
      </div>
    </>
  );
}
