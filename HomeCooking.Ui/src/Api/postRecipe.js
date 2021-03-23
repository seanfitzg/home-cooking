import config from '../config';

const url = config[process.env.REACT_APP_ENV].recipes_api;

const postRecipe = async (recipe, getAccessTokenSilently) => {
  const options = {
    audience: 'https://home-cooking/api',
    scope: 'read:recipes',
  };
  const accessToken = await getAccessTokenSilently(options);
  const request = {
    method: 'post',
    body: JSON.stringify(recipe),
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${accessToken}`,
    },
  };

  const res = await fetch(url, request);
  return res;
};

export default postRecipe;
