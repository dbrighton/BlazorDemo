namespace Common.Models;

public record GenericErrorAction(string Message);

public record GenericWarningAction(string Message);

public record GenericInfoAction(string Message);

public record GenericSuccessAction(string Message);
public record GenericDarkAction(string Message);
public record GenericPrimaryAction(string Message);
public record GenericLightAction(string Message);
public record GenericLinkAction(string Message);