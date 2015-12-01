// An analog signal is acquired through the Analog PIN 0 (A0).  
// Its digital value (that goes from 0 to 1023) is scaled to
// have the range (0 to 255). This byte is sent by the Serial port to the PC where it's received by Unity and 
// it made changes in the Scene (see Unity project)

int value = 0;

// the setup routine runs once when you press reset:
void setup() 
{
  // initialize serial communication at 9600 bits per second:
  Serial.begin(9600);
}

// the loop routine runs over and over again forever:
void loop() {
  // read the input on analog pin 0:
  int sensorValue = analogRead(A0); //int is a int16, and analogRead returns in16 between 0 and 1023 (ADC 10bits resolution 2^10 )
  delay(100);        // delay in between reads
  value = sensorValue / 4;
  Serial.write(byte(value));
  //Serial.println(sensorValue,DEC);
}
