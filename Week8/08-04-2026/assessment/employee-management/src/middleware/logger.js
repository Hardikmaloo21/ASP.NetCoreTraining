const logger = store => next => action => {
  console.group(`%c ▶ ${action.type}`, 'color:#3b82f6;font-weight:700;font-size:12px');
  console.log('%c Prev State', 'color:#94a3b8;font-weight:600', store.getState());
  console.log('%c Payload   ', 'color:#06b6d4;font-weight:600', action.payload);
  const result = next(action);
  console.log('%c Next State', 'color:#10b981;font-weight:600', store.getState());
  console.groupEnd();
  return result;
};

export default logger;