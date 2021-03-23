import config from '../config';

const url = config[process.env.REACT_APP_ENV].recipes_api;

const getRecipeById = async (getAccessTokenSilently, id) => {
  const options = {
    audience: 'https://home-cooking/api',
    scope: 'read:recipes',
  };
  const accessToken = await getAccessTokenSilently(options);
  const request = {
    method: 'get',
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${accessToken}`,
    },
  };
  const res = await fetch(`${url}/${id}`, request);
  return await res.json();
};

export default getRecipeById;
