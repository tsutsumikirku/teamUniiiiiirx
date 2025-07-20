public static class BranchOutGameOver
{
    public enum BranchOutGameOverType
    {
        MentalBreakGameOver = 1,
        ForcedRemovalGameOver = 2,
        NormalGameClear = 3,
        RichGameClear = 4
    }
    public static BranchOutGameOverType GetGameOverType(int mental, int money)
    {
        if (mental <= 0)
        {
            return BranchOutGameOverType.MentalBreakGameOver;
        }
        else if (money < 100000)
        {
            return BranchOutGameOverType.ForcedRemovalGameOver;
        }
        else if (money >= 500000)
        {
            return BranchOutGameOverType.RichGameClear;
        }
        else
        {
            return BranchOutGameOverType.NormalGameClear;
        }
    }
}
