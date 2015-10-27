using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class StretchFactor : MonoBehaviour {

	public float exgValue = 1.0f;
	private Vector3 originalScale;
	private float mu = 0.19f;
	private Transform tx;
	private Vector3 velocity = Vector3.zero;
	private Vector3 pVec;
	private Vector3 eVec;
	private Vector3 lVec;
	private Vector3 nVec;
	private Vector3 newRotation;
	private SkinnedMeshRenderer maleMesh;
	GameObject parent;
	Vector3 sVec;
	Vector3 tVec;
	float Sradius;
	float Sphi;
	float Stheta;
	float Tradius;
	float Tphi;
	float Ttheta;
	//Vector3 centreOfMale;
	private Vector3 newScale;	

	// Use this for initialization
	void Start () {
		originalScale = transform.localScale;   //to reset scale after every update
		//print (originalScale);
		tx = transform;
		maleMesh = GameObject.Find("Skin").GetComponent<SkinnedMeshRenderer> ();
		//centreOfMale = maleMesh.bounds.extents;
		parent = GameObject.Find ("Parent").gameObject;
	}

	
	// Update is called once per frame
	void Update () {	
			newScale = calcWaveFields ();
			//newScale.z = originalScale.z *(A.z); 
			transform.localScale = Vector3.SmoothDamp(transform.localScale,newScale, ref velocity,0.005f); //updating new scale 
			//Update values to GUI
			float height = newScale.y; 
			float width = newScale.x;
			//getting the canvas text component
			Text yValue = GameObject.Find("yValue").GetComponent<Text>(); //height
			//converting float value to string
			string newYValue = height.ToString();
			//assigning string to Text field
			yValue.text = "Height: " + newYValue; 
			Text xValue = GameObject.Find("xValue").GetComponent<Text>();
			string newXValue = width.ToString();
			xValue.text = "Width: " + newXValue; 
			Text exg = GameObject.Find("exg").GetComponent<Text>();
			string exgString = exgValue.ToString();
			exg.text = exgString;
			//Text mr = GameObject.Find("mr").GetComponent<Text>();
			//string mrString = mu.ToString();
			///mr.text = mrString;
					
	}	
	public Vector3 calcWaveFields()
	{	
		//defining the vectors N,L,P,E and R
		//getting x and z position coordinates of the human model
		float x = tx.position.x;
		float z = tx.position.z;
		float y = tx.position.y;
		Vector3 n;
		//N vector is from centre of human model to centre of BBH
		if (gameObject.CompareTag("sphere")) {
			n = new Vector3 (-x, -y / 2, -z);  
		} else {
			n = new Vector3 (-x, -y, -z);
		}
		//print ("nvector " + n);
		Vector3 nVec = Vector3.Normalize (n); // Unit vector of N
		//print (nVec);
		Vector3 lVec = new Vector3 (0, 1, 0); // L unit vector
		Vector3 p = Vector3.Cross (nVec, lVec); // P = LxN
		float pMag = Vector3.Magnitude (p);
		Vector3 pVec;
		if (pMag < 0.000001f) {
			pVec = new Vector3(1,0,0);
		} else {
			pVec = p / pMag;
		}
		//print ("p Vector " + pVec);
		print (" P vector " + pVec);
		Vector3 eVec = Vector3.Cross (pVec, nVec); // E = PxN
		// transform positions of the binary black holes
		GameObject BH1 = GameObject.Find ("BH1");
		GameObject BH2 = GameObject.Find ("BH2");
		Vector3 sphere1 = BH1.transform.position; //BH1 is sphere1 
		Vector3 sphere2 = BH2.transform.position; 
		//distance between BH2 and BH1
		float ax = sphere2.x - sphere1.x;
		float ay = sphere2.y - sphere1.y;
		float az = sphere2.z - sphere1.z;
		//Defining R vector
		Vector3 rVec = new Vector3 (ax,ay,az);
		//angle between the R and Principle direction(P) phi(t) in radians
		float phi = Vector3.Angle (pVec, rVec)*Mathf.Deg2Rad;
		float phid = phi * Mathf.Rad2Deg;
		//distance from the source 
		float D = Vector3.Magnitude (n); 
		//binary separation
		float r = Vector3.Distance(sphere1,sphere2); 
		float c = 2 * mu / (r * D); //constant
		//calculating H PLUS  polarization field  
		// psi is the angle in the orbital plane from the principal+ direction +N x L 
		float cphi = Mathf.Cos (phi);	//cos psi
		float sphi = Mathf.Sin (phi);
		float cos2phi = (2 * cphi * cphi) - 1;
		//print ("capital phi" + phid );
		float sin2phi = 2 * sphi * cphi;

		float ln = Vector3.Dot (lVec, nVec);
		float hPlus = -c * (1 +(ln*ln)) * cos2phi;
		// calculating H CROSS and PLUS polarization field
		print ("hplus " + hPlus);
		float hCross = -c * (-2 * Vector3.Dot (lVec, nVec)) * sin2phi;
		print ("hcross " + hCross);
		float tan2theta = hCross / hPlus;
		float theta = Mathf.Atan2 (hCross, hPlus)/2;
		print ("theta : " + theta*Mathf.Rad2Deg);
		sVec = (pVec * Mathf.Cos (theta)) + (eVec * Mathf.Sin (theta));
		print ("s vector is" + sVec);
		tVec = Vector3.Cross (sVec, nVec);
		print ("t vector is" + tVec);
		cartToSpher (sVec, tVec);
		float Afactor = Mathf.Sqrt (hPlus * hPlus + hCross * hCross);
		Afactor *= exgValue;

		//displaying values in UI
		Text exagValue = GameObject.Find("Exagvalue").GetComponent<Text>();
		exagValue.text = "Exaggeration: "+exgValue.ToString();
		Text Avalue = GameObject.Find("Afactor").GetComponent<Text>();
		Avalue.text = "Stretch Factor: "+Afactor.ToString();

		//applying stretch factor to the scale
		newScale = originalScale;
		if (hPlus < 0) { 		
			newScale.x = newScale.x * (1.0f - (Afactor));   //S = H(1+A)
			newScale.y = newScale.y * (1.0f + (Afactor));
		} else {
			newScale.x = newScale.x * (1.0f + (Afactor));   //S = H(1+A)
			newScale.y = newScale.y * (1.0f - (Afactor));
		}
		return newScale;
	
	}

		public void cartToSpher(Vector3 Svec, Vector3 Tvec){
		Vector3 t = Tvec;
		Vector3 s = Svec;
		Sradius = Mathf.Sqrt (s.x*s.x + s.y*s.y + s.z*s.z);
		Stheta = Mathf.Atan2 (s.y, s.x);
		Sphi = Mathf.Acos (s.z / Sradius);
		Vector3 Srot;
		Srot.x = parent.transform.localEulerAngles.x;
		Srot.z = Stheta*Mathf.Rad2Deg;
		Srot.y = Sphi * Mathf.Rad2Deg;
		parent.transform.localEulerAngles = new Vector3 (Srot.x, Srot.y, Srot.z);
		print ("theta " + Stheta);
		print ("phi" + Sphi);
		this.transform.parent = parent.transform;
	}

	public void adjustMu(float newMu) //dynamic variable
	{
		mu = newMu;

	}
	public void adjustExag(float newExag) // getting exaggeration factor from GUI 
	{
		exgValue = newExag;
	}


}
