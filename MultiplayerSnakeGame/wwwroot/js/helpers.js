export const $ = document.querySelector.bind(document)
export const $$ = document.querySelectorAll.bind(document)

export const capitalize = string => string.charAt(0).toUpperCase() + string.slice(1)
export const getRouteParam = paramName => new URLSearchParams(window.location.search).get(paramName)