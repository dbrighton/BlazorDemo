namespace UserFeatureModule.Store;

public record OnLoginAction(Person User, bool IsLoggedIn);

public record OnLogoutAction(Person User, bool IsLoggedIn);