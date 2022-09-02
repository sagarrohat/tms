export const getAssignableUsers = (state: Object) => {
  var assignableUsers = state.home.assignableUsers;
  return [
    {
      FullName: "All Users",
      UserId: "*",
    },
    {
      FullName: "Unassigned",
      UserId: null,
    },
    ...assignableUsers.map((item) => ({
      FullName: `${item.FirstName} ${item.LastName}`,
      UserId: item.Id,
    })),
  ];
};

export const getTasks = (state: Object) => state.home.tasks;

export const getTaskListFilters = (state: Object) => state.home.filters;

export const getActiveTab = (state: Object) => state.home.activeTab;
