/**
 * Copyright 2017-2018 Plexus Interop Deutsche Bank AG
 * SPDX-License-Identifier: Apache-2.0
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
package com.db.plexus.interop.dsl.validation.rules

import org.eclipse.xtext.resource.XtextResourceSet
import com.db.plexus.interop.dsl.gen.GenUtils
import com.google.inject.Inject
import static com.db.plexus.interop.dsl.validation.Issues.*;
import com.db.plexus.interop.dsl.protobuf.Method

class NoMethodTypesChangedRule implements UpdateRule {

    val GenUtils genUtils;

    @Inject
    new(GenUtils genUtils) {
        this.genUtils = genUtils;
    }

    override getCode() '''service-method-updated'''

    override validate(XtextResourceSet baseResourceSet, XtextResourceSet updatedResourceSet) {
        val baseMethods = genUtils.getServiceMethodsMap(baseResourceSet.resources);
        val updatedMethods = genUtils.getServiceMethodsMap(updatedResourceSet.resources);
        val updatedIds = updatedMethods.keySet;
        return baseMethods.entrySet
        .filter[methodEntry | updatedIds.contains(methodEntry.key)]
        .filter(baseMethodEntry | !methodsEqual(baseMethodEntry.value, updatedMethods.get(baseMethodEntry.key)))
        .map([methodEntry | createError('''Service Method «methodEntry.key» is updated''', getCode)])
        .toList()
    }

    def methodsEqual(Method first, Method second) {

        if(!genUtils.getFullName(first.request.message)
        .equals(genUtils.getFullName(second.request.message))) {
            return false;
        }

        if(!first.request.isStream.equals(second.request.isStream)) {
            return false;
        }

        if(!genUtils.getFullName(first.response.message)
        .equals(genUtils.getFullName(second.response.message))) {
            return false;
        }

        if(!first.response.isStream.equals(second.response.isStream)) {
            return false;
        }

        return true;
    }

}