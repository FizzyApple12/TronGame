

using System.Numerics;

public class Protocol
{
    public delegate void NullEvent();
    public delegate void IDEvent(int id);
    public delegate void PointsEvent(int points);
    public delegate void WinLossEvent(bool win);
    public delegate void PosRotEvent(Vector3 position, Quaternion rotation);
    public delegate void PosDirEvent(Vector3 position, Vector3 direction);

    public event NullEvent OnHeartBeat;

    public event IDEvent OnWhoAmI;
    public event IDEvent OnWhoAreThey;

    public event IDEvent OnNextPlayerID;
    public event PosRotEvent OnHeadPosRotEvent;
    public event PosRotEvent OnChestPosRotEvent;
    public event PosRotEvent OnLeftElbowPosRotEvent;
    public event PosRotEvent OnLeftHandPosRotEvent;
    public event PosRotEvent OnRightElbowPosRotEvent;
    public event PosRotEvent OnRightHandPosRotEvent;
    public event PosRotEvent OnHipsPosRotEvent;
    public event PosRotEvent OnLeftKneePosRotEvent;
    public event PosRotEvent OnLeftFootPosRotEvent;
    public event PosRotEvent OnRightKneePosRotEvent;
    public event PosRotEvent OnRightFootPosRotEvent;
    public event PointsEvent OnDisplayPointsEvent;
    public event WinLossEvent OnDisplayWinStatusEvent;
    public event PosRotEvent OnKillPlaneEvent;

    public event IDEvent OnNextDiscID;
    public event PosRotEvent OnDiscPosRotEvent;

    public event PosDirEvent OnBouncePosDirEvent;

    int messageStep = 0; // start with a data header of 0x69
    byte command = 0x00; // command 

    enum CommandType
    {
        HeartBeat = 0x00, // heartbeat

        WhoAmI = 0x01,     // who am i? (get your player id)
        WhoAreThey = 0x02, // who are they? (get every other player id)

        ReadyStartGame = 0x03, // ready/start game

        // player data
        NextPlayerID = 0x10,     // next player id
        HeadPosRot = 0x11,       // head position + rotation
        ChestPosRot = 0x12,      // chest position + rotation
        LeftElbowPosRot = 0x13,  // left elbow position + rotation
        LeftHandPosRot = 0x14,   // left hand position + rotation
        RightElbowPosRot = 0x15, // right elbow position + rotation
        RightHandPosRot = 0x16,  // right hand position + rotation
        HipsPosRot = 0x17,       // hips position + rotation
        LeftKneePosRot = 0x18,   // left knee position + rotation
        LeftFootPosRot = 0x19,   // left foot position + rotation
        RightKneePosRot = 0x1a,  // right knee position + rotation
        RightFootPosRot = 0x1b,  // right foot position + rotation
        DisplayPoints = 0x1c,    // display points
        DisplayWinStatus = 0x1d, // display win status
        KillPlane = 0x1f,        // kill across position + rotation
        
        // disc data
        NextDiscID = 0x30, // next disc id
        DiscPosRot = 0x31, // disc position + rotation
        ThrowDisc = 0x32,  // throw disc
        CatchDisc = 0x33,  // catch disc

        // misc effects
        BouncePosDir = 0x50, // bounce at position + direction

        Acknowledgement = 0xff
    }


    static byte[] data = new byte[28];
    /*
     * might follow a few formats [4 bytes per segment for 32-bit floats and ints]
     * [ , , , , , , ]
     * [id, , , , , , ]
     * [points, , , , , , ]
     * [win/loss, , , , , , ]
     * [vx,vy,vz,qx,qy,qz,qw]
     * [vx,vy,vz,vx,vy,vz, ]
     */

    public void ConsumeByte(byte inByte)
    {
        // this function does all of the data processing so we don't have to worry about variable length messages

        if (inByte == 0x69)
            messageStep = 1;

        if (messageStep == 0)
            return; // still awaitng a message to be started

    }

    public static void SendDiscThrow(Disc disc)
    {

    }
}
