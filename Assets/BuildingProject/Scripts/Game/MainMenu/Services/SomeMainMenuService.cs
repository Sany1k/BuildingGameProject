using UnityEngine;

public class SomeMainMenuService
{
    private readonly SomeCommonService someCommonService;

    public SomeMainMenuService(SomeCommonService someCommonService)
    {
        this.someCommonService = someCommonService;
        Debug.Log(GetType().Name + " has been created");
    }
}
