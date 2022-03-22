export const POP_MESSAGE = "POP_MESSAGE";
export const POP_ALL_MESSAGES = "POP_ALL_MESSAGES";
export const PUSH_MESSAGE = "PUSH_MESSAGE";

export const messageReducer = (state, action) => {
  let tempMessages = state.messages;
  switch (action.type) {
    case POP_ALL_MESSAGES:
      return { messages: [] };
    case POP_MESSAGE:
      tempMessages.shift();
      return { ...state, messages: tempMessages };
    case PUSH_MESSAGE:
      tempMessages.push(action.payload);
      return { ...state, messages: tempMessages };
    default:
      break;
  }
};
