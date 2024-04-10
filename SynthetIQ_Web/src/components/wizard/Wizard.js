
//
// This component manages the overall state of the wizard, including which step is currently active.
// 

import React, { useState } from 'react';
import IntroductionComponent from './IntroductionComponent';
import ProjectDescriptionComponent from './ProjectDescriptionComponent';
import AssistantConfigurationComponent from './AssistantConfigurationComponent';
import SummaryAndConfirmationComponent from './SummaryAndConfirmationComponent';
import { Button } from '@material-ui/core';

const Wizard = () => {
  const [activeStep, setActiveStep] = useState(0);

  const handleNext = () => setActiveStep((prevActiveStep) => prevActiveStep + 1);
  const handleBack = () => setActiveStep((prevActiveStep) => prevActiveStep - 1);

  const getStepComponent = (stepIndex) => {
    switch (stepIndex) {
      case 0:
        return <IntroductionComponent onStart={handleNext} />;
      case 1:
        return <ProjectDescriptionComponent onContinue={handleNext} onBack={handleBack} />;
      case 2:
        return <AssistantConfigurationComponent onContinue={handleNext} onBack={handleBack} />;
      case 3:
        return <SummaryAndConfirmationComponent onBack={handleBack} />;
      default:
        return 'Unknown Step';
    }
  };

  return (
    <div>
      {getStepComponent(activeStep)}
      {activeStep !== 0 && (
        <div>
          <Button onClick={handleBack} disabled={activeStep === 0}>
            Back
          </Button>
          <Button variant="contained" color="primary" onClick={handleNext}>
            {activeStep === 3 ? 'Finish' : 'Next'}
          </Button>
        </div>
      )}
    </div>
  );
};

export default Wizard;
