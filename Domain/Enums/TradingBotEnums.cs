namespace Domain.Enums
{
    public enum TradeBotChannelInfinityType
    {
        None = 0,
        Defined = 1, // Strogo definirani kanal sa zadanom početnom točkom i SlotSize-om
        Infinite = 2, // Infinite kanal koji samo gleda trenutnu cijenu i na temelju nje i SlotSize-a računa početnu i završnu točku
                      // za kanal ovog tipa bitno je imati neograničeno pozicija i QuoteAsseta za održavanje kanala
    }

    public enum TradeBotChannelStructureType
    {
        None = 0,
        Horizontal = 1,
        Inclined = 2,
    }

    public enum TradeBotExecutionType
    {
        None = 0,
        Immediately = 1,
        OnPrice = 2,
        OnTime = 3
    }

    public enum TradeBotMarketDirection
    {
        None = 0,
        BOTH = 1,
        LONG = 2,
        SHORT = 3
    }

    public enum TradeBotMarketType
    {
        None = 0,
        SPOT = 1,
        USDFUTURES = 2
    }

    public enum TradeBotOrderType
    {
        Nonem = 0,
        Position = 1,
        LimitOrder = 2,
        TakeProfitOrder = 3,
        StopLossOrder = 4
    }

    public enum TradeBotQuantityType
    {
        None = 0,
        BaseAssetAmount = 1,
        QuoteAssetAmount = 2,
    }

    public enum TradeBotSlotType
    {
        None = 0,
        Fixed = 1,
        Percentage = 2,
    }

    public enum TradeBotStatus
    {
        None = 0,
        Initialised = 1, // Početno stanje bota
        WaitingToBeTriggered = 2,
        Running = 3, // Označeno triggerom
        Paused = 4, // Može se pauzirati iz bilo kojeg razloga
        Ended = 5 // Neaktivno (arhivirano) stanje bota...mora se ručno aktivirati
    }

    public enum TradeBotTriggerType
    {
        None = 0,
        StartBot = 1, // Bot je u početku initialised, treba ga startati trigerom / ili je bot Endan (arhiviran) pa ga je potrebno opet pokrenuti.
        EndBot = 2, // Bot može automatski ući u ended state, koje je konačno.
        PauseBot = 3,
        UnpauseBot = 4,
        ChangeQuantity = 5,
        ChangeChannelOrSlots = 6
    }
}
