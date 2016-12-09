export const getBaseUrl = function () {
    var url = window.location.protocol + "//" + window.location.host + window.location.pathname + "/";
    return url;
}

export default getBaseUrl;