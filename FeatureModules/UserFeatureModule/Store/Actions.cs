using System.Security.Claims;

namespace UserFeatureModule.Store;

public record AuthHubSetConnectedAction(bool Connected);

public record AuthHubStartAction;

public record UserLoginSuccessAction(ClaimsPrincipal User);
public record UserLogoutSuccessAction();