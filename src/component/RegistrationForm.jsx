import React, { useState } from "react";
import "./RegistrationForm.css";

function RegistrationForm({ goToHome }) {
  const [formData, setFormData] = useState({
    fullName: "",
    fatherName: "",
    motherName: "",
    dob: "",
    gender: "",
    maritalStatus: "",
    spouseName: "",
    email: "",
    mobile: "",
    emergencyContact: "",
    permanentAddress: "",
    currentAddress: "",
    department: "",
    designation: "",
    salary: "",
    pfNumber: "",
    panNumber: ""
  });

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const payload = {
      fullName: formData.fullName,
      fatherName: formData.fatherName,
      motherName: formData.motherName,

      // ✅ safe date format
      dateOfBirth: formData.dob ? formData.dob : null,

      gender: formData.gender,
      maritalStatus: formData.maritalStatus,
      spouseName: formData.spouseName,

      email: formData.email,
      mobileNumber: formData.mobile,
      emergencyContact: formData.emergencyContact,

      permanentAddress: formData.permanentAddress,
      currentAddress: formData.currentAddress,

      department: formData.department,

      // ✅ FIXED mapping
      role: formData.designation,

      salary: Number(formData.salary) || 0,

      pfNumber: formData.pfNumber,

      // ✅ FIXED backend field name
      taxNumber: formData.panNumber
    };

    console.log("PAYLOAD:", payload);

    try {
      const response = await fetch(
        "https://localhost:7247/api/EmployeeApi",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify(payload)
        }
      );

      const resultText = await response.text();

      if (!response.ok) {
        console.log("ERROR:", resultText);
        alert("Error: " + resultText);
        return;
      }

      alert("Employee Registered Successfully");

      // reset form
      setFormData({
        fullName: "",
        fatherName: "",
        motherName: "",
        dob: "",
        gender: "",
        maritalStatus: "",
        spouseName: "",
        email: "",
        mobile: "",
        emergencyContact: "",
        permanentAddress: "",
        currentAddress: "",
        department: "",
        designation: "",
        salary: "",
        pfNumber: "",
        panNumber: ""
      });

      if (goToHome) {
        goToHome();
      }
    } catch (error) {
      console.error(error);
      alert("Backend not reachable");
    }
  };

  return (
    <div className="container">
      <div className="card">
        <h1>Employee Registration Form</h1>

        <form onSubmit={handleSubmit}>
          {/* PERSONAL */}
          <div className="section">
            <h2>Personal Information</h2>

            <div className="grid">
              <input type="text" name="fullName" placeholder="Full Name" value={formData.fullName} onChange={handleChange} />

              <input type="text" name="fatherName" placeholder="Father Name" value={formData.fatherName} onChange={handleChange} />

              <input type="text" name="motherName" placeholder="Mother Name" value={formData.motherName} onChange={handleChange} />

              <input type="date" name="dob" value={formData.dob} onChange={handleChange} />

              <select name="gender" value={formData.gender} onChange={handleChange}>
                <option value="">Select Gender</option>
                <option value="Male">Male</option>
                <option value="Female">Female</option>
                <option value="Other">Other</option>
              </select>

              <select name="maritalStatus" value={formData.maritalStatus} onChange={handleChange}>
                <option value="">Marital Status</option>
                <option value="Single">Single</option>
                <option value="Married">Married</option>
              </select>

              {formData.maritalStatus === "Married" && (
                <input
                  type="text"
                  name="spouseName"
                  placeholder="Spouse Name"
                  value={formData.spouseName}
                  onChange={handleChange}
                />
              )}
            </div>
          </div>

          {/* CONTACT */}
          <div className="section">
            <h2>Contact Information</h2>

            <div className="grid">
              <input type="email" name="email" placeholder="Email" value={formData.email} onChange={handleChange} />

              <input type="text" name="mobile" placeholder="Mobile Number" value={formData.mobile} onChange={handleChange} />

              <input type="text" name="emergencyContact" placeholder="Emergency Contact" value={formData.emergencyContact} onChange={handleChange} />
            </div>
          </div>

          {/* ADDRESS */}
          <div className="section">
            <h2>Address Information</h2>

            <div className="grid">
              <textarea name="permanentAddress" placeholder="Permanent Address" value={formData.permanentAddress} onChange={handleChange} />

              <textarea name="currentAddress" placeholder="Current Address" value={formData.currentAddress} onChange={handleChange} />
            </div>
          </div>

          {/* EMPLOYMENT */}
          <div className="section">
            <h2>Employment Information</h2>

            <div className="grid">
              <input type="text" name="department" placeholder="Department" value={formData.department} onChange={handleChange} />

              <input type="text" name="designation" placeholder="Role / Designation" value={formData.designation} onChange={handleChange} />

              <input type="number" name="salary" placeholder="Salary" value={formData.salary} onChange={handleChange} />

              <input type="text" name="pfNumber" placeholder="PF Number" value={formData.pfNumber} onChange={handleChange} />

              <input type="text" name="panNumber" placeholder="PAN Number" value={formData.panNumber} onChange={handleChange} />
            </div>
          </div>

          <button type="submit">Register Employee</button>
        </form>
      </div>
    </div>
  );
}

export default RegistrationForm;