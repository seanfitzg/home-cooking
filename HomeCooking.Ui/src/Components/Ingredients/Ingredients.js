import * as React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { DataGrid } from '@material-ui/data-grid';

const useStyles = makeStyles((theme) => ({
  title: {
    marginBottom: 5,
  },
  container: {
    width: '100%',
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
  return (
    <div className={classes.container}>
      <div className={classes.title}>Ingredients</div>
      <div style={{ width: '100%' }}>
        <DataGrid
          rows={rows}
          columns={columns}
          pageSize={100}
          checkboxSelection
          autoHeight
          autoPageSize
          hideFooterPagination
        />
      </div>
    </div>
  );
}
