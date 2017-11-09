using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UDPServer
{
    class Controller
    {
        private enum State { IDLE, NOT_IDLE };
        private State state = State.IDLE;


        
        public FormMain formMain = null;
        public Server_v2 serverPtr = null;

        private int databaseCounter = 0;
        private const int DATABASE_UPDATE_INTERVAL = 300; // Select period in seconds

        public void ControllerTask()
        {

            switch (state)
            {
                case State.IDLE:
                    //Start server
                    formMain.startUDPServer2();
                    state = State.NOT_IDLE;
                    return;
                case State.NOT_IDLE:
                    // Do some task
                    UpdateDatabaseTask();
                    serverTask();
                    return;
                default:
                    break;
            }

        } // End controller task


        private void UpdateDatabaseTask()
        {
            databaseCounter++;
            if (databaseCounter >= DATABASE_UPDATE_INTERVAL)
            {
                formMain.UpdateDatabaseTask();
                databaseCounter = 0;
            }
        }

        private void serverTask()
        {
            if (formMain.ServerIsStoped)
            {
                return; // Server is manualy stopped, dont try to recover it
            }

            if (!formMain.ValidateInput())
            {
                return; // Invalid port number, dont try to recover it
            }

            if (!Server_v2.isActive)
            {
                // Its not active and its not stopped, try to restart it
                formMain.startUDPServer2();
            }

        }

    }
}
