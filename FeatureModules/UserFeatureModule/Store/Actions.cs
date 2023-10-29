namespace UserFeatureModule.Store;

public record AuthHubSetConnectedAction(bool Connected);

public record AuthHubStartAction();
public record AuthHubReceiveUserAction(Person Person);
public record AuthHubSendUserAction(int Count);
public record AuthHubSendUserFailedAction(string Message);