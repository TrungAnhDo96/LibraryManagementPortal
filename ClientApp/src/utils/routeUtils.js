export function getRouteNames(routes) {
  let result = [];
  routes.forEach((route) => {
    result.push(route.name);
  });
  return result;
}
