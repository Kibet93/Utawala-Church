/*
 * @bot-written
 *
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
 * commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
 * licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
 * including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
 * access, download, storage, and/or use of this source code.
 *
 * BOT WARNING
 * This file is bot-written.
 * Any changes out side of "protected regions" will be lost next time the bot makes any changes.
 */
import { Symbols } from 'Symbols';
import { initValidators, IModelAttributeValidationError, ErrorType } from '../Util';
import moment from 'moment';
import { Model } from 'Models/Model';

export default function validate() {
	return (target: object, key: string) => {
		initValidators(target, key);
		target[Symbols.validatorMap][key].push('Date');
		target[Symbols.validator].push(
			(model: Model): Promise<IModelAttributeValidationError | null> => new Promise((resolve) => {
				if(model[key] === null || model[key] === undefined) {
					resolve(null);
				} else {
					resolve(
						moment(model[key], 'YYYY-MM-DD').isValid() 
							? null 
							: 
							{
								errorType: ErrorType.INVALID,
								errorMessage: `The value ${model[key]} is not in the format YYYY-MM-DD`, 
								attributeName: key, 
								target: model 
							}
					); 
				}
			}),
		);
	};
}