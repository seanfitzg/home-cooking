import * as React from 'react';
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
  { field: 'ingredient', headerName: 'Ingredient', width: 400 },
  { field: 'amount', headerName: 'Amount', width: 200 },
];

const rows = [
  { id: 1, ingredient: 'Beans', amount: '2 cans' },
  { id: 2, ingredient: 'Sliced Pan', amount: '1' },
];

export default function Ingredients() {
  const classes = useStyles();
  const [ingredient, setIngredient] = useState('');
  const [amount, setAmount] = useState('');
  const [deleteDisabled, setDeleteDisabled] = useState(true);
  const [ingredientList, setIngredientList] = useState(rows);
  const [itemsToDelete, setItemsToDelete] = useState([]);
  const handleAdd = () => {
    if (!ingredient) return;
    setIngredientList([
      ...ingredientList,
      { id: uuidv4(), ingredient, amount },
    ]);
    setAmount('');
    setIngredient('');
  };

  const handleDelete = () => {
    var remaining = ingredientList.filter(
      (el) => !itemsToDelete.includes(el.id)
    );
    setIngredientList(remaining);
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
              id="ingredient"
              label="Ingredient"
              value={ingredient}
              onChange={(event) => setIngredient(event.target.value)}
            />
            <TextField
              id="amount"
              label="Amount"
              value={amount}
              onChange={(event) => setAmount(event.target.value)}
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
