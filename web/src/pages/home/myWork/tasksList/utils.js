export const formatName = function (userName: string) {
  return userName
    .split(" ")
    .map((n) => n[0])
    .join("");
};
